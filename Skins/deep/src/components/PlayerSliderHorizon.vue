<template>
  <div
    class="slider-wrap"
    :class="{'slider-wrap--active': isActive}"
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
      isActive: false
    }
  },

  methods: {
    setPosition({clientX}) {
      if(clientX === 0) return;

      const slider = this.$el;
      const {width: sliderWidth, left: sliderLeft} = slider.getBoundingClientRect();
      const positionRatio = ((clientX - sliderLeft) / sliderWidth ) * 100;

      this.widthPercentage = positionRatio >= 100 ? 100 : positionRatio <= 0 ? 0 : positionRatio;
    },

    mouseDown(e) {
      this.isActive = true;
      this.setCapture();
      this.setPosition(e);
      this.$emit('slider-thumb-down', this.widthPercentage);
    },

    mouseMove(e) {
      this.setPosition(e);
      this.$emit('slider-thumb-move', this.widthPercentage);
    },

    mouseUp() {
      this.isActive = false;
      this.$emit('slider-thumb-up')
    },

    setCapture() {
      const mouseMoveListener = (e) => {
        e.stopPropagation();
        e.preventDefault();

        this.mouseMove(e);
      }

      const mouseUpListener = (e) => {
        e.stopPropagation();
        e.preventDefault();
        window.removeEventListener('mousemove', mouseMoveListener, true);
        window.removeEventListener('mouseup', mouseUpListener, true);

        this.mouseUp();
      }

      window.addEventListener('mousemove', mouseMoveListener, true);
      window.addEventListener('mouseup', mouseUpListener, true);
    },
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
    &::before {
      display: block;
    }
  }
  }
}

.slider-wrap--active {
  .slider-location {
    background: $signature-color;
    &::before {
      display: block;
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