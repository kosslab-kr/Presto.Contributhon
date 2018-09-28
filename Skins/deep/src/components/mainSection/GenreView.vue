<template>
  <section class="genre-view">
    <div class="genre-view__header">
      <div class="genre-view__title">{{title}}</div>
      <div class="genre-view__menu">
        <ViewHeaderMenuItem
          v-for="(genre, idx) in genres"
          :key="idx"
          :name="genre.type"
          ref="menuItems"
          @menu-selected="_showContent"/>
      </div>
    </div>
    <section class='genre-view__body'>
      <MusicList
        v-for="(genre, idx) in genres"
        :key="idx"
        :name="genre.type"
        :musics="genre.musics"
        ref="musicList"
        @playQueue-played="_playGenre"/>
    </section>
  </section>
</template>

<script>
import ViewHeaderMenuItem from './ViewHeaderMenuItem.vue';
import MusicList from './MusicList.vue';

export default {
  name: 'GenreView',
  
  components: {
    ViewHeaderMenuItem,
    MusicList
  },

  props: {
    genres: {
      type: Array,
      default() {
        return [];
      }
    }
  },

  data() {
    return {
      title: 'Genres'
    }
  },

  // initialize
  mounted() {
    if(!this.genres[0]) return;

    const clickEvent = new Event('click');
    const firstMenuItem = this.$refs.menuItems[0];

    firstMenuItem.$el.dispatchEvent(clickEvent);
  },

  methods: {
    _showContent(name) {
      this.$el.scrollTo(0,0);
      this._activateMenu(name);
      this._activateMusicList(name);
    },

    _activateMenu(name) {
      this.$refs.menuItems.forEach(menu => {
        menu.isActive = (menu.name === name) ? true : false;
      });
    },

    _activateMusicList(name) {
      this.$refs.musicList.forEach(musicList => {
        musicList.isActive = (musicList.name === name) ? true : false;
      })
    },

    _playGenre(playQueue) {
      this.$emit('genre-played', playQueue);
    }
  }
}
</script>

<style scope lang="scss">
@import '../../index.scss';

.genre-view {
  width: 100%;
  height: 100%;
  overflow: scroll;
  background: #191919;

  &:before {
    content: '';
    display: block;
    height: 100px;
  }
}

.genre-view__header {
  position: sticky;
  top: 0px;
  border-bottom: 1px solid #333;
  z-index: 1;
}

.genre-view__title {
  box-sizing: border-box;
  color: #fff;
  padding: 0px 30px 20px 30px;
  font-size: 2.8rem;
  font-weight: 900;
  background: #191919;

  &:hover {
    cursor: default;
  }
}

.genre-view__menu {
  box-sizing: border-box;
  background: #191919;
  padding: 0px 30px 2px 30px;
  white-space: nowrap;
  overflow: scroll;
}

.genre-view__body {
  padding: 30px;
}

</style>
