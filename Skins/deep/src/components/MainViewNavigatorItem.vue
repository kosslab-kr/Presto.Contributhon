<template>
  <div
    class="navigator-item"
    @click="$emit('item-selected', name)"
  >
    <div class="navigator-item__name">{{name}}</div>
    <div class="navigator-item__bar"></div>
  </div>
</template>

<script>
export default {
  name: 'ViewHeaderMenuItem',
  
  props: {
    name: String
  }
}
</script>

<style scoped lang="scss">
@import '../index.scss';

.navigator-item {
  display: inline-block;
  margin-right: 25px;

  &:nth-last-child(1) {
    margin-right: 0px;
  }

  &--active {
    .navigator-item__name {
      color: #fff;
    }

    .navigator-item__bar {
      background: $signature-color;
    }
  }

  &:hover {
    cursor: pointer;

    .navigator-item__name {
      color: #fff;
    }
  }
}

.navigator-item__name {
  color: #aaa;
  font-size: 0.8rem;
  font-weight: 600;
  letter-spacing: 2px;
}

.navigator-item__bar {
  margin: auto;
  margin-top: 10px;
  width: 30px;
  height: 2px;
}
</style>
