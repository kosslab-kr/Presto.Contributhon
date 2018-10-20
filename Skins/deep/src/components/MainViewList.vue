<template>
  <div class="list" :style="{'height': height}">
    <div class="list__field-wrap">
      <div
        class="list__field"
        v-for="(field, index) in fields"
        :key="index"
        :style="{'width': field.width, 'text-align': field.textAlign, 'margin-right': field.marginRight}"
      >{{field.name}}</div>
    </div>
    <MainViewListItem
      v-for="(item, index) in items"
      :key="index"
      :item="item"
      :fields="fields"
      @music-played="$emit('music-played', $event)"
      @context-menu-opened="$emit('context-menu-opened', $event)"
    />
  </div>
</template>

<script>
import MainViewListItem from './MainViewListItem.vue';

export default {
  name: 'MainViewList',

  components: {
    MainViewListItem
  },

  props: {
    items: {
      type: Array,
      default() {
        return [
          {
            'matchedToFieldValue': '',
          }
        ]
      }
    },
    fields: {
      type: Array,
      default() {
        return [
          {
            name: '',
            value: 'matchedToFieldValue',
            width: '0%',
            textAlign: 'center',
            marginRight: '0%',
            playButton: false
          }
        ]
      }
    },
    height: {
      type: String,
      default: 'auto'
    }
  }
}
</script>

<style scoped lang="scss">
@import '../index.scss';

.list {
  width: 100%;
  background: #191919;
  overflow: scroll;
}

.list__field-wrap {
  position: sticky;
  top: 0px;
  background: #191919;
  border-bottom: 1px solid #333;
  overflow: hidden;
  z-index: 1;
}

.list__field {
  float: left;
  height: 20px;
  line-height: 20px;
  color: #999;
  font-size: 0.9rem;
}

</style>
