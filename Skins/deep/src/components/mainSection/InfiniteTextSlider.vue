<template>
  <div ref="gallery" class="gallery">
    <div class="wrap" :class="{'wrap--active': onPlay}" :style="{'--slider-destination': '-' + sliderDistance + 'px', '--slider-duration': sliderDuration + 's'}">
      <div ref="text" class="text" :style="fontStyle">{{text}}</div>
      <div v-if="isTextOverflowed" class="text text-copy" :style="fontStyle">{{text}}</div>
    </div>
  </div>
</template>

<script>
export default {
  name: 'InfiniteTextSlider',

  props: {
    text: String,

    fontStyle: {
      type: Object,
      default() {
        return {
          'color': '#fff',
          'font-size': '1rem',
          'font-weight': 'normal'
        }
      }
    }
  },

  data() {
    return {
      onPlay: false,
      isTextOverflowed: false,
      sliderDistance: 0
    }
  },

  methods: {
    activate() {
      window.addEventListener('resize', this._setAnimation.bind(this));

      setTimeout(this._setAnimation.bind(this));
    },

    inactivate() {
      window.removeEventListener('resize', this._setAnimation.bind(this));
    },
    
    _setAnimation() {
      // debugger;
      const gallery = this.$refs.gallery;
      const text = this.$refs.text;
      const gap = text.clientWidth - gallery.offsetWidth;

      if(gap > 0) {
        this.isTextOverflowed = true;
        this.sliderDistance = text.offsetWidth;
      }
      else {
        this.isTextOverflowed = false;
        this.sliderDistance = 0;
      }
    }
  },

  computed: {
    sliderDuration() {
      return this.sliderDistance/40;
    }
  }
}
</script>

<style scoped lang="scss">
@import '../../index.scss';

@keyframes slide {
  100% {
    transform: translate3d(var(--slider-destination), 0, 0);
  }
}

.gallery {
  width: 100%; height: 100%;
  white-space: nowrap;
  overflow: hidden;
}

.wrap--active {
  animation: slide var(--slider-duration) infinite linear;
}

.text {
  display: inline-block;
  border-right: 2rem solid transparent;
}

</style>
