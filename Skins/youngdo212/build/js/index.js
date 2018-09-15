import {DummyCore} from './dummyCore.js';

Vue.component('horizon-slider', {
  props: ['initialWidthPercentage'],

  data() {
    return {
      widthPercentage: this.initialWidthPercentage || 0
    }
  },

  methods: {
    setPosition({currentTarget, clientX}) {
      if(clientX === 0) return;
      
      const {width: sliderWidth, left: sliderLeft} = currentTarget.getBoundingClientRect();
      const positionRatio = ((clientX - sliderLeft) / sliderWidth ) * 100;

      this.widthPercentage = positionRatio >= 100 ? 100 : positionRatio <= 0 ? 0 : positionRatio;
    },

    setDragImage(event) {
      const invisibleElem = document.createElement('img');
      event.dataTransfer.setDragImage(invisibleElem, 0, 0);
    },

    holdSliderThumb(e) {
      this.setPosition(e);
      this.$emit('hold-slider-thumb', this.widthPercentage);
    },

    releaseSliderThumb() {
      this.$emit('release-slider-thumb')
    }
  },

  template: `
  <div
    class="player__slider-wrap"
    draggable='true'
    @dragstart='setDragImage'
    @mousedown='holdSliderThumb'
    @drag='holdSliderThumb'
    @mouseup='releaseSliderThumb'
    @dragend='releaseSliderThumb'>
    <div class="player__slider">
      <div
        :style='{width: widthPercentage + "%"}'
        class="player__slider-location">
      </div>
    </div>
  </div>`
})

const core = new DummyCore({
  musicList: [{runningTime: 210000}]
})

const player = new Vue({
  el: '#player',

  data: {
    core: core,
    currentMusic: null,
    currentTime: 0,
    onPlay: false
  },

  created() {
    this.currentMusic = this.core.currentMusic;
    this.core.onReturnCurrentTime = this.setCurrentTime.bind(this);
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

    calcCurrentTime(widthPercentage) {
      this.currentTime = (widthPercentage / 100) * this.currentMusic.runningTime;
    },

    setCurrentTime(time) {
      this.currentTime = time;
      this.$refs.slider.widthPercentage = ( this.currentTime / this.currentMusic.runningTime) * 100;
    },
    
    togglePlay() {
      this.onPlay = !this.onPlay;
      this.onPlay ? this.run() : this.pause();
    },

    run() {
      this.core.play();
    },

    pause() {
      this.core.pause();
    }
  },

  computed: {
    runningTimePercentage() {
      return ( this.currentTime / this.currentMusic.runningTime) * 100;
    }
  }
})