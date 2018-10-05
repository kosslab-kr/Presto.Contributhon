<template>
  <MainView name="Albums">
    <template slot="body">
      <MainViewGroup
        v-for="(item, index) in items"
        :key="index"
        type="album"
        :group="item"
        @picture-selected="selectedAlbum = $event"
        @group-played="$emit('music-played', $event)"
      />
    </template>
    <template slot="popup">
      <transition name="fade">
        <div
          v-if="!!selectedAlbum"
          class="popup-background"
        />
      </transition>
      <transition name="slide-fade">
        <div
          v-if="!!selectedAlbum"
          class="popup-wrap"
        >
          <div class="popup">
            <MainViewPopup
              type="album"
              :group="selectedAlbum"
              @popup-closed="selectedAlbum = null"
              @group-played="$emit('music-played', $event)"
            >
              <template slot="body">
                <MainViewList
                  :items="albumListItems"
                  :fields="albumListFields"
                  :height="'100%'"
                  @music-played="$emit('music-played', {currentMusicIdx: $event, musics: selectedAlbum.musics})"
                />
              </template>
            </MainViewPopup>
          </div>
        </div>
      </transition>
    </template>
  </MainView>
</template>

<script>
import MainView from './MainView.vue';
import MainViewGroup from './MainViewGroup';
import MainViewPopup from './MainViewPopup.vue';
import MainViewList from './MainViewList.vue';

export default {
  name: 'MainAlbums',

  components: {
    MainView,
    MainViewGroup,
    MainViewPopup,
    MainViewList
  },

  props: {
    items: Array
  },

  data() {
    return {
      selectedAlbum: null,
      albumListFields: [
        {
          name: '#',
          value: 'number',
          width: '10%',
          textAlign: 'center',
          marginRight: '3%',
          playButton: true
        },
        {
          name: 'TITLE',
          value: 'title',
          width: '70%',
          textAlign: 'left',
          marginRight: '0%'
        },
        {
          name: 'TIME',
          value: 'time',
          width: '17%',
          textAlign: 'center',
          marginRight: '0%'
        }
      ]
    }
  },

  methods: {
    formatTime(milliseconds) {
      const date = new Date(milliseconds);
      const minutes = date.getMinutes();
      const seconds = date.getSeconds();
      const formattedMinutes = minutes.toString();
      const formattedSeconds = seconds.toString().padStart(2, 0);

      return `${formattedMinutes}:${formattedSeconds}`;
    },
  },

  computed: {
    albumListItems() {
      const musics = this.selectedAlbum.musics;

      return musics.map((music, index) => {
        return {number: index+1, title: music.title, time: this.formatTime(music.runningTime), source: index};
      })
    }
  }
}
</script>

<style scoped lang="scss">
@import '../index.scss';

.popup-background {
  position: fixed;
  top: 0px; left: $main-menu-width;
  width: calc(100vw - #{$main-menu-width}); height: calc(100vh - #{$player-height});
  background: rgba(0,0,0,0.6);
  z-index: 100;
}

.popup-wrap {
  @extend .popup-background;
  background: transparent;
  text-align: center;
  
  // vertical align helper
  &::before {
    content: '';
    display: inline-block;
    height: 100%;
    vertical-align: middle;
  }
}

.popup {
  display: inline-block;
  vertical-align: middle;
}

.fade-enter, .fade-leave-to {
  opacity: 0;
}

.fade-enter-active, .fade-leave-active {
  transition: opacity .5s;
}

.slide-fade-enter, .slide-fade-leave-to {
  opacity: 0;
  transform: translate3d(0, -70px, 0);
}

.slide-fade-enter-active, .slide-fade-leave-active {
  transition: opacity .5s, transform .5s;
}

</style>