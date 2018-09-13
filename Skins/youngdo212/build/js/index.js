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
  
Vue.component('player-slider', {
  data() {
    return {
      widthPercentage: 0
    }
  },
  methods: {
    setPosition({currentTarget, clientX}) {
      const {width: sliderWidth, left: sliderLeft} = currentTarget.getBoundingClientRect();
      const positionRatio = ((clientX - sliderLeft) / sliderWidth ) * 100;

      this.widthPercentage = positionRatio;
    }
  },
  template: `
  <div
    class="player__slider-wrap"
    @mousedown='setPosition'>
    <div class="player__slider">
      <div
        :style='{width: widthPercentage + "%"}'
        class="player__slider-location">
      </div>
    </div>
  </div>`
})

const player = new Vue({ el: '#player'})