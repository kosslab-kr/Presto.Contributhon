<template>
  <div
    class="menu-list-item"
    :class="{'menu-list-item--selected': isActive}"
    @click="$emit('menu-selected', name)">
    <div class="menu-list-item__name">{{name}}</div>
    <div class="menu-list-item__bar"></div>
  </div>
</template>

<script>
export default {
  name: 'ViewHeaderMenuItem',
  
  props: {
    name: String
  },

  data() {
    return {
      isActive: false
    }
  }
}
</script>

<style scoped lang="scss">
@import '../../index.scss';

.menu-list-item {
  display: inline-block;
  margin-right: 25px;

  &:nth-last-child(1) {
    margin-right: 0px;
  }

  &--selected {
    .menu-list-item__name {
      color: #fff;
    }

    .menu-list-item__bar {
      background: $signature-color;
    }
  }

  &:hover {
    cursor: pointer;

    .menu-list-item__name {
      color: #fff;
    }
  }
}

.menu-list-item__name {
  color: #aaa;
  font-size: 0.8rem;
  font-weight: 600;
  letter-spacing: 2px;
}

.menu-list-item__bar {
  margin: auto;
  margin-top: 10px;
  width: 30px;
  height: 2px;
}
</style>
