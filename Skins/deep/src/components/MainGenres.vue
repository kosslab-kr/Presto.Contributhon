<template>
  <MainView :name="name">
    <template slot="navigator">
      <MainViewNavigator
        :itemNames="items.map(item => item.name)"
        @item-selected="selectGenre"
      />
    </template>
    <template slot="body">
      <MainViewList
        v-if="selectedGenre"
        :items="genreListItems"
        :fields="genreListFields"
        @music-played="$emit('music-played', $event)"
      />
    </template>
  </MainView>
</template>

<script>
import MainView from './MainView.vue';
import MainViewNavigator from './MainViewNavigator.vue';
import MainViewList from './MainViewList.vue';

export default {
  name: 'MainGenres',

  components: {
    MainView,
    MainViewNavigator,
    MainViewList
  },

  props: {
    name: String,
    items: Array
  },

  data() {
    return {
      selectedGenreName: '',
      genreListFields: [
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
    selectGenre(genreName) {
      this.selectedGenreName = genreName;
      this.$el.scrollTo(0,0);
    },

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
    selectedGenre() {
      for(let genre of this.items) {
        if(genre.name === this.selectedGenreName) return genre;
      }

      return null;
    },

    genreListItems() {
      const musics = this.selectedGenre.musics;

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
