<template>
  <MainView :name="name">
    <template slot="body">
      <MainViewList
        v-if="items.length > 0"
        :items="playlistItems"
        :fields="playlistFields"
        @music-played="$emit('music-played', $event)"
      />
    </template>
  </MainView>
</template>

<script>
import MainView from './MainView.vue';
import MainViewList from './MainViewList.vue';

export default {
  name: 'MainPlaylist',

  components: {
    MainView,
    MainViewList
  },

  props: {
    name: String,
    items: Array,
  },

  data() {
    return {
      playlistFields: [
        {
          name: '',
          value: 'number',
          width: '6%',
          textAlign: 'center',
          marginRight: '2%',
          playButton: true
        },
        {
          name: 'TITLE',
          value: 'title',
          width: '39%',
          textAlign: 'left',
          marginRight: '5%'
        },
        {
          name: 'ARTIST',
          value: 'artist',
          width: '14%',
          textAlign: 'left',
          marginRight: '5%'
        },
        {
          name: 'ALBUM',
          value: 'album',
          width: '14%',
          textAlign: 'left',
          marginRight: '5%'
        },
        {
          name: 'TIME',
          value: 'time',
          width: '5%',
          textAlign: 'center',
          marginRight: '0%'
        },
        {
          name: '',
          value: 'more',
          width: '5%',
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
    playlistItems() {
      const musics = this.items;

      return musics.map((music) => {
        return {
          number: '',
          title: music.title,
          artist: music.artist.name,
          album: music.album.name,
          time: this.formatTime(music.length),
          more: '',
          source: music
        };
      })
    }
  }
}
</script>

<style scoped lang="scss">

</style>
