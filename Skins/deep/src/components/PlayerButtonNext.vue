<template>
  <div
    class="button-wrap"
    @mouseenter="isActive = true"
    @mouseleave="isActive = false"
    @mousedown="isActive = false"
    @click="clickButton"
  >
    <svg
      version="1.1"
      baseProfile="full"
      width="100%" height="100%" viewBox="0 0 100 100"
      xmlns="http://www.w3.org/2000/svg"
    >
      <line x1="70" y1="20" x2="70" y2="80" :stroke="isActive ? '#fff' : '#aaa'" stroke-width="12"/>
      <polygon points="20 20, 20 80, 70 50" :fill="isActive ? '#fff' : '#aaa'"/>
    </svg>
  </div>
</template>

<script>
export default {
  name: 'PlayerButtonNext',

  data() {
    return {
      isActive: false
    }
  },

  methods: {
    clickButton() {
      this.isActive = true;
      this.$emit('button-clicked');
    }
  }
}
</script>

<style scoped lang="scss">
@import '../index.scss';

.button-wrap {
  display: inline-block;
  width: 18px; height: 18px;
  vertical-align: middle;
  margin-right: 23px;
}

</style>
