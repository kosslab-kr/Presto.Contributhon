<template>
  <div
    class="menu-item"
    :class="{'menu-item--selected': isActive, 'menu-item--pressed': isPressed}"
    @mousedown="isPressed = true"
    @click="activate">
    {{name}}
  </div>
</template>

<script>
export default {
  name: 'MainMenuItem',

  props: {
    name: String
  },

  data() {
    return {
      isActive: false,
      isPressed: false
    }
  },

  methods: {
    activate() {
      this.isPressed = false;
      this.isActive = true;
      this.$emit('menu-clicked', this.name);
    }
  }
}
</script>

<style scoped lang="scss">
@import '../../index.scss';

.menu-item {
  box-sizing: border-box;
  padding: 0px 25px;
  margin: 0.7rem 0;
  line-height: 1.3;
  color: #aaa;

  &:hover { color: #fff; }
}

.menu-item--selected {
  color: #fff;
  box-shadow: inset 4px 0px 0 0 $signature-color;
}

.menu-item--pressed {
  color: #aaa !important;
}

</style>
