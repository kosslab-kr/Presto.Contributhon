<template>
  <div
    class="context-menu"
  >
    <div
      class="context-menu__item"
      v-for="(item, index) in items"
      :key="index"
      @click.stop="clickItem(item.callback)"
      @mouseenter="activatedSubContextMenuNumber = index"
      @mouseleave="activatedSubContextMenuNumber = null"
    >
      <div class="context-menu__item-name">
        {{item.name}}
        <div
          v-if="!!item.subItems"
          class="context-menu__more-icon"
        />
      </div>
      <BaseContextMenu
        v-if="!!item.subItems"
        v-show="activatedSubContextMenuNumber === index"
        :style="{position: 'absolute', top: '-10px', left: '100%'}"
        :items="item.subItems"
        @item-clicked="$emit('item-clicked')"
      />
      <div
        class="context-menu__border"
        v-if="item.border && (index !== (items.length-1))"
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
      border: {Boolean} - optional
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

  updated() {
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
  width: 200px;
  padding: 10px 0;
  background: #272727;
  border-radius: 8px;
  box-shadow: 0px 0px 20px 2px #111;
  z-index: 1000;
}

.context-menu__item {
  position: relative;
  box-sizing: border-box;
  line-height: 1.8rem;
  font-weight: lighter;
  font-size: 0.9rem;

  &:hover {
    > .context-menu__item-name {
      color: #fff;
      background: #555;
    }
  }
}

.context-menu__item-name {
  position: relative;
  color: #ccc;
  padding: 0 20px;
  white-space: nowrap;
  overflow: hidden;
  text-overflow: ellipsis;
}

.context-menu__more-icon {
  position: absolute;
  top: 0px; right: 10px;
  width: 20px; height: 100%;
  @include line-arrow($size: 10px, $border-width: 1px, $direction: right, $color: #ccc)
}

.context-menu__border {
  width: 100%; height: 0px;
  border-bottom: 1px solid #444;
  margin: 5px 0px;
}
</style>
