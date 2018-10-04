<template>
  <div class="navigator">
    <MainViewNavigatorItem
      ref="items"
      v-for="(itemName, index) in itemNames"
      :key="index"
      :name="itemName"
      :class="{'navigator-item--active': currentItemName === itemName}"
      @item-selected="activateItem"
    />
  </div>
</template>

<script>
import MainViewNavigatorItem from './MainViewNavigatorItem.vue'

export default {
  name: 'MainViewNavigator',
  
  components: {
    MainViewNavigatorItem
  },

  props: {
    itemNames: {
      type: Array,
      default() { return ['EMPTY'] }
    }
  },

  data() {
    return {
      currentItemName: ''
    }
  },

  // initialize
  mounted() {
    const clickEvent = new Event('click');
    const firstItem = this.$refs.items[0];

    firstItem.$el.dispatchEvent(clickEvent);
  },

  methods: {
    activateItem(itemName) {
      this.currentItemName = itemName;
      this.$emit('item-selected', itemName);
    }
  }
}
</script>

<style scope lang="scss">
@import '../index.scss';

.navigator {
  box-sizing: border-box;
  background: #191919;
  padding: 0px 30px 2px 30px;
  white-space: nowrap;
  overflow: scroll;
}

</style>