<template>
  <div ref="gallery" class="gallery">
    <div class="wrap" :class="{'wrap--active': onPlay && isTextOverflowed}" :style="{'--slider-destination': '-' + sliderDistance + 'px', '--slider-duration': sliderDuration + 's', '--slider-delay': delay +'s'}">
      <div ref="text" class="text" :style="fontStyle">{{text}}</div>
      <div v-if="onPlay && isTextOverflowed" class="text text-copy" :style="fontStyle">{{text}}</div>
    </div>
  </div>
</template>

<script>
export default {
  name: 'BaseTextRolling',

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
    },

    delay: {
      type: Number,
      default: 0
    },

    onPlay: {
      type: Boolean,
      default: false
    }
  },

  data() {
    return {
      sliderDistance: 0
    }
  },

  mounted() {
    this.sliderDistance = this.$refs.text.offsetWidth;
  },

  computed: {
    sliderDuration() {
      return this.sliderDistance/40;
    },

    isTextOverflowed() {
      if(!this.onPlay) return false;

      const gallery = this.$refs.gallery;
      const text = this.$refs.text;
      const gap = text.clientWidth - gallery.offsetWidth;

      return gap > 0;
    }
  }
}
</script>

<style scoped lang="scss">
@import '../index.scss';

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
  animation: slide var(--slider-duration) infinite linear var(--slider-delay);
}

.text {
  display: inline-block;
  border-right: 2em solid transparent;
}

</style>
