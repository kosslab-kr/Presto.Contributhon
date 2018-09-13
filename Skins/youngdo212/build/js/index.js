const playButton = new Vue({
  el: '#play-button',
  data: {
    onPlay: false
  },
  computed: {
    classObject() {
      return {
        'player__button': true,
        'player__button--play': !this.onPlay, 
        'player__button--pause': !!this.onPlay
      };
    }
  },
  methods: {
    toggle() {
      this.onPlay = !this.onPlay;
    }
  }
})