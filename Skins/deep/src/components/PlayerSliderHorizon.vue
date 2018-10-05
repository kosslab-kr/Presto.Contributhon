<template>
  <div
    class="slider-wrap"
    @mousedown='mouseDown'
    >
    <div class="slider">
      <div
        :style='{width: widthPercentage + "%"}'
        class="slider-location">
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: 'PlayerSliderHorizon',

  data() {
    return {
      widthPercentage: 0,
      isMousePressed: false
    }
  },

  mounted() {
    document.addEventListener('mousemove', (e) => {
      if(!this.isMousePressed) return;
      this.holdSliderThumb({currentTarget: this.$el, clientX: e.clientX});
    });

    document.addEventListener('mouseup', () => {
      if(!this.isMousePressed) return;
      this.releaseSliderThumb()
    })
  },

  methods: {
    setPosition({currentTarget, clientX}) {
      if(clientX === 0) return;
      
      const {width: sliderWidth, left: sliderLeft} = currentTarget.getBoundingClientRect();
      const positionRatio = ((clientX - sliderLeft) / sliderWidth ) * 100;

      this.widthPercentage = positionRatio >= 100 ? 100 : positionRatio <= 0 ? 0 : positionRatio;
    },

    setDragImage(event) {
      this.$emit('touch-slider-thumb');
      const invisibleElem = document.createElement('img');
      event.dataTransfer.setDragImage(invisibleElem, 0, 0);
    },

    mouseDown(e) {
      this.isMousePressed = true;
      this.$emit('touch-slider-thumb');
      this.holdSliderThumb(e);
    },

    holdSliderThumb(e) {
      if(!this.isMousePressed) return;
      this.setPosition(e);
      this.$emit('hold-slider-thumb', this.widthPercentage);
    },

    releaseSliderThumb() {
      this.isMousePressed = false;
      this.$emit('release-slider-thumb')
    }
  }
}
</script>

<style scoped lang="scss">
@import '../index.scss';

.slider-wrap {
  display: inline-block;
  position: relative;
  width: 100%; height: 100%;
  
  &:hover {
    .slider-location {
      background: $signature-color;

      // slider-thumb
      &::before { display: block }
    }
  }
}

.slider {
  position: absolute;
  top: 50%;
  transform: translate3d(0, -50%, 0);
  width: 100%; height: 4px;
  background: #3a3a3a;
  border-radius: 5px;
}

.slider-location {
  position: relative;
  height: 100%;
  border-radius: 5px;
  background: #ccc;

  // slider-thumb
  &::before {
    content:'';
    position: absolute;
    top: 0px; left: 100%;
    display: none;
    width: 12px; height: 12px; background: #fff;
    border-radius: 50%;
    transform: translate3d(-50%, -35%, 0);
  }
}
</style>