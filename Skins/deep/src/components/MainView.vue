<template>
  <section class="view">
    <div class="view__header">
      <div class="view__name">{{name}}</div>
      <slot name="navigator"></slot>
    </div>
    <div class='view__body'>
      <slot name="body"></slot>
    </div>
    <slot name="popup"></slot>
  </section>
</template>

<script>
export default {
  name: 'MainView',

  props: {
    name: String
  }
}
</script>

<style scope lang="scss">
@import '../index.scss';

.view {
  position: relative;
  width: 100%;
  height: 100%;
  overflow: scroll;
  background: #191919;

  &:before {
    content: '';
    display: block;
    height: 100px;
  }
}

.view__header {
  position: sticky;
  top: 0px;
  border-bottom: 1px solid #333;
  z-index: 10;
}

.view__name {
  box-sizing: border-box;
  color: #fff;
  padding: 0px 30px 20px 30px;
  font-size: 2.8rem;
  font-weight: 900;
  background: #191919;

  &:hover {
    cursor: default;
  }
}

.view__body {
  padding: 30px;
}

</style>
