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
      if(clientX === 0) return;
      
      const {width: sliderWidth, left: sliderLeft} = currentTarget.getBoundingClientRect();
      const positionRatio = ((clientX - sliderLeft) / sliderWidth ) * 100;

      this.widthPercentage = positionRatio >= 100 ? 100 : positionRatio <= 0 ? 0 : positionRatio;
    },
    setDragImage(event) {
      const invisibleElem = document.createElement('img');
      event.dataTransfer.setDragImage(invisibleElem, 0, 0);
    }
  },
  template: `
  <div
    class="player__slider-wrap"
    draggable='true'
    @mousedown='setPosition'
    @drag='setPosition'
    @dragstart='setDragImage'>
    <div class="player__slider">
      <div
        :style='{width: widthPercentage + "%"}'
        class="player__slider-location">
      </div>
    </div>
  </div>`
})

const player = new Vue({ el: '#player'})