<template>
  <svg class="button"
    version="1.1"
    baseProfile="full"
    width="100%" height="100%" viewBox="0 0 100 100"
    xmlns="http://www.w3.org/2000/svg"
    :class="{'button--pressed': isButtonPressed}"
    :style="{'--background': background, '--hoverBackground': hoverBackground}"
    @mousedown.stop="isButtonPressed = true"
    @mouseleave.stop="isButtonPressed = false"
    @click.stop="clickButton">
    <polygon v-if="!onPlay" points="39 31, 72 50, 39 69" fill="#fff"/>
    <line v-if="onPlay" x1="39" y1="31" x2="39" y2="69" stroke="#fff" stroke-width="9"/>
    <line v-if="onPlay" x1="61" y1="31" x2="61" y2="69" stroke="#fff" stroke-width="9"/>
    <rect v-if="isButtonPressed" width="100%" height="100%" fill="rgba(0,0,0,0.3)"/>
  </svg>
</template>

<script>
export default {
  name: 'BaseButtonPlayPause',

  props: {
    onPlay: {
      type: Boolean,
      default: false
    },

    background: {
      type: String,
      default: 'transparent'
    },

    hoverBackground: {
      type: String,
      default: 'transparent'
    }
  },

  data() {
    return {
      isButtonPressed: false,
    }
  },

  methods: {
    clickButton() {
      this.isButtonPressed = false;
      this.$emit('button-clicked');
    }
  }
}
</script>

<style scoped lang="scss">
@import "../index.scss";

.button {
  box-shadow: inset 0px 0px 0px 1px #bbb;
  border-radius: 50%;
  transition: transform 0.05s;
  background: var(--background);

  &:hover {
    box-shadow: inset 0px 0px 0px 1px #fff;
    transform: scale(1.1);
    cursor: default;
    background: var(--hoverBackground);
  }
}

.button--pressed {
  transform: scale(1) !important;
  border-color: #bbb !important;
}

</style>
