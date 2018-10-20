<template>
  <MainView :name="name">
    <template slot="body">
      <MainViewGroup
        v-for="(item, index) in items"
        :key="index"
        type="artist"
        :group="item"
        @picture-selected="selectedArtist = $event"
        @group-played="$emit('music-played', $event)"
      />
    </template>
    <template slot="popup">
      <transition name="fade">
        <div
          v-if="!!selectedArtist"
          class="popup-background"
        />
      </transition>
      <transition name="slide-fade">
        <div
          v-if="!!selectedArtist"
          class="popup-wrap"
        >
          <div class="popup">
            <MainViewPopup
              type="artist"
              :group="selectedArtist"
              @popup-closed="selectedArtist = null"
              @group-played="$emit('music-played', $event)"
            >
              <template slot="body">
                <MainViewList
                  :items="artistListItems"
                  :fields="artistListFields"
                  :height="'100%'"
                  @music-played="$emit('music-played', $event)"
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
    name: String,
    items: Array
  },

  data() {
    return {
      selectedArtist: null,
      artistListFields: [
        {
          name: '#',
          value: 'number',
          width: '10%',
          textAlign: 'center',
          marginRight: '2%',
          playButton: true
        },
        {
          name: 'TITLE',
          value: 'title',
          width: '42%',
          textAlign: 'left',
          marginRight: '3%'
        },
        {
          name: 'ALBUM',
          value: 'album',
          width: '22%',
          textAlign: 'left',
          marginRight: '3%'
        },
        {
          name: 'TIME',
          value: 'time',
          width: '10%',
          textAlign: 'center',
          marginRight: '0%'
        },
        {
          name: '',
          value: 'more',
          width: '7%',
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
    artistListItems() {
      const musics = this.selectedArtist.musics;

      return musics.map((music, index) => {
        return {
          number: index+1,
          title: music.title,
          album: music.album.name,
          time: this.formatTime(music.length),
          more: '',
          source: music,
        };
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
