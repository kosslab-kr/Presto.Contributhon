<template>
  <section class="main-section">
    <header class='main-section__header'>
      <Search/>
      <div class="main-section__profile-more"></div>
    </header>
    <SubSection/>
  </section>
</template>

<script>
import SubSection from './SubSection.vue';
import Search from './Search.vue';

export default {
  name: 'MainSection',
  components: {
    SubSection,
    Search
  }
}
</script>

<style scoped lang="scss">
@import '../../index.scss';

.main-section {
  @include position(absolute, $top: 0px, $left: $main-menu-width);
  width: calc(100vw - #{$main-menu-width}); height: calc(100vh - #{$player-height});
  overflow: hidden;
}

.main-section__header {
  @include position(fixed, $top: 0px, $left: $main-menu-width);
  width: calc(100vw - #{$main-menu-width}); height: 46px;
  box-sizing: border-box;
  padding: 15px 30px;
  z-index: 1;
}

.main-section__header-category {
  height: 100%;
  
  &--left { float: left }

  &--right { float: right }
}

.main-section__move {
  float: left;
  position: relative;
  width: 26px; height: 26px;

  &--back {
    @include line-arrow($size: 50%, $border-width: 1px, $direction: left, $color: #fff);
  }

  &--forward {
    @include line-arrow($size: 50%, $border-width: 1px, $direction: right, $color: #fff);
  }
}

.main-section__upgrade {
  float: left;
  box-sizing: border-box;
  height: 100%;
  background: #222;
  border: 1px solid #aaa;
  border-radius: 20px;
  padding: 0px 25px;
  margin-right: 20px;
  font-size: 0.8rem;
  font-weight: bold;
  color: #fff;
  letter-spacing: 2px;
  transition: transform 0.1s;

  @include vertical-align-helper;

  &:hover {
    cursor: default;
    transform: scale(1.07);
    border-color: #fff;
  }
}

.main-section__profile {
  float: left;
  height: 100%;
  color: #fff;
  margin-right: 20px;
  
  @include vertical-align-helper;

  &:hover {
    cursor: default;
    text-decoration: underline;
  }
}

.main-section__profile-more {
  float: left;
  position: relative;
  width: 26px; height: 26px;
  @include line-arrow($size: 50%, $border-width: 1px, $direction: bottom, $color: #fff);
}
</style>