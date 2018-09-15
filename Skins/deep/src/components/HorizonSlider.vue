<template>
  <div
    class="slider-wrap"
    draggable='true'
    @dragstart='setDragImage'
    @mousedown='holdSliderThumb'
    @drag='holdSliderThumb'
    @mouseup='releaseSliderThumb'
    @dragend='releaseSliderThumb'>
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
  }
}
</script>

<style scoped lang="scss">
@import '../index.scss';

.slider-wrap {
  box-sizing: border-box;
  display: inline-block;
  width: calc(95% - #{$player-time-size * 2}); height: 100%;
  padding: 8px 0;
  vertical-align: middle;
  
  &:hover {
    .slider-location {
      background: $signature-color;

      // slider-thumb
      &::before { display: block }
    }
  }
}

.slider {
  height: 100%;
  background: #3a3a3a;
  border-radius: 4px;
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