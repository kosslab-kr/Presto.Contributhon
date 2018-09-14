Vue.component('play-button', {
  data() {
    return {
      onPlay: false
    }
  },
  methods: {
    toggle() {
      this.onPlay = !this.onPlay;
    }
  },
  template: `
  <div
    class='player__button'
    :class='onPlay ? "player__button--pause" : "player__button--play"'
    @click='toggle'>
    </div>
    `
  })
  
Vue.component('horizon-slider', {
  data() {
    return {
      widthPercentage: 0
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

const player = new Vue({
  el: '#player',

  data: {
    currentMusic: {runningTime: 200000},
    currentTime: 0
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
    }
  }
})