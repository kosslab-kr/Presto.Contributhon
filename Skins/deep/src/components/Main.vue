<template>
  <section class="main">
    <header class='main__header'>
      <MainSearch
        @value-changed="searchItem"
      />
      <div
        class="more-menu-button"
        :style="{opacity: isMoreMenuPressed ? '0.7' : '1'}"
        @mousedown="isMoreMenuPressed = true"
        @mouseleave="isMoreMenuPressed = false"
        @click="openMoreMenu"
      />
      <MainMoreMenu
        v-if="isMoreMenuOpened"
        @menu-closed="closeMoreMenu"
      />
    </header>
    <component
      :name="currentViewName"
      :is="currentViewComponentName"
      :items="currentViewItems"
      @music-played="playMusic"
    />
  </section>
</template>

<script>
import MainSearch from './MainSearch.vue';
import MainMoreMenu from './MainMoreMenu.vue';
import MainMusics from './MainMusics.vue';
import MainAlbums from './MainAlbums.vue';
import MainGenres from './MainGenres.vue';
import MainArtists from './MainArtists.vue';
import MainPlaylist from './MainPlaylist.vue';

export default {
  name: 'Main',

  components: {
    MainSearch,
    MainMoreMenu,
    MainMusics,
    MainAlbums,
    MainGenres,
    MainArtists,
    MainPlaylist,
  },

  data() {
    return {
      isMoreMenuPressed: false,
      isMoreMenuOpened: false,
      currentViewName: '',
      currentViewComponentName: '',
      currentViewItemsAll: null,
      currentViewItems: null,
    }
  },

  methods: {
    playMusic(music) {
      this.$emit('music-played', music);
    },

    showView(view) {
      this.currentViewName = view.name;
      this.currentViewComponentName = view.componentName;
      this.currentViewItemsAll = view.items;
      this.currentViewItems = this.currentViewItemsAll;
    },

    searchItem({target}) {
      const keyword = target.value;
      const keywordLowerCase = keyword.toLowerCase();

      this.currentViewItems = this.currentViewItemsAll.filter(item => {
        const targetName = item.title ? item.title :
                           item.name ? item.name :
                           item.type;
                           
        const targetNameLowerCase = targetName.toLowerCase();

        return targetNameLowerCase.includes(keywordLowerCase);
      })
    },

    openMoreMenu() {
      this.isMoreMenuPressed = false;
      this.isMoreMenuOpened = true;
    },

    closeMoreMenu() {
      this.isMoreMenuOpened = false;
    }
  }
}
</script>

<style scoped lang="scss">
@import '../index.scss';

.main {
  @include position(absolute, $top: 0px, $left: $main-menu-width);
  width: calc(100vw - #{$main-menu-width}); height: calc(100vh - #{$player-height});
  overflow: hidden;
}

.main__header {
  @include position(fixed, $top: 0px, $left: $main-menu-width);
  width: calc(100vw - #{$main-menu-width}); height: 70px;
  background: linear-gradient(#191919 50%, rgba(0,0,0,0));
  box-sizing: border-box;
  padding: 15px 30px 0 30px;
  z-index: 100;
}

.more-menu-button {
  position: relative;
  float: left;
  width: 26px; height: 26px;
  margin-left: 5px;
  @include line-arrow($size: 50%, $border-width: 1px, $direction: bottom, $color: #fff);
}

</style>