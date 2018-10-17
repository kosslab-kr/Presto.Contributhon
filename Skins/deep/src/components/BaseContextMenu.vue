<template>
  <div
    class="context-menu"
  >
    <div
      class="context-menu__item"
      v-for="(item, index) in items"
      :key="index"
      @click="clickItem(item.callback)"
      @mouseenter="activatedSubContextMenuNumber = index"
      @mouseleave="activatedSubContextMenuNumber = null"
    >
      {{item.name}}
      <BaseContextMenu
        v-if="!!item.subItems"
        v-show="activatedSubContextMenuNumber === index"
        :style="{position: 'absolute', top: '-10px', left: '100%'}"
        :items="item.subItems"
        @item-clicked="$emit('item-clicked')"
      />
    </div>
  </div>
</template>

<script>
export default {
  name: "BaseContextMenu",

  props: {
    /*
    {
      name: {String},
      callback: {Function} - optional,
      subItems: {Array} - optional
    }
    */
    items: Array,
  },

  data() {
    return {
      activatedSubContextMenuNumber: null,
    }
  },

  created() {
    window.onwheel = (e) => { e.preventDefault(); };
    window.onkeydown = (e) => { e.preventDefault(); };
    window.addEventListener('click', this.emitOutsideClickEvent, true);
    window.addEventListener('contextmenu', this.emitOutsideClickEvent, true);
  },

  destroyed() {
    window.onwheel = null;
    window.onkeydown = null;
    window.removeEventListener('click', this.emitOutsideClickEvent, true);
    window.removeEventListener('contextmenu', this.emitOutsideClickEvent, true);
  },

  methods: {
    clickItem(callback) {
      // when subContextMenu exist
      if(!callback) return;

      callback();
      this.$emit('item-clicked');
    },

    emitOutsideClickEvent({target}) {
      if(this.$el.contains(target)) return;

      this.$emit('outside-clicked');
    }
  }
}
</script>

<style scoped lang="scss">
@import '../index.scss';

.context-menu {
  box-sizing: border-box;
  position: fixed;
  width: 160px;
  padding: 10px 0;
  background: #272727;
  border-radius: 8px;
  box-shadow: 0px 0px 20px 2px #111;
  z-index: 1000;
}

.context-menu__item {
  position: relative;
  box-sizing: border-box;
  padding: 0 10px;
  line-height: 1.8rem;
  color: #fff;
  font-weight: lighter;
  font-size: 0.9rem;

  &:hover {
    background: #555;
  }
}

</style>
