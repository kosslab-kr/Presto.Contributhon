<template>
  <section class="main-section">
    <header class='main-section__header'>
      <div class="main-section__header-category main-section__header-category--left">
        <div class="main-section__move main-section__move--back"></div>
        <div class="main-section__move main-section__move--forward"></div>
        <form class="main-section__search-form" action='' method='get'>
          <div class="main-section__search-icon"></div>
          <input class='main-section__search-input' type="text" name='keyword' value='Search'>
        </form>
      </div>
      <div class="main-section__header-category main-section__header-category--right">
        <div class="main-section__upgrade">UPGRADE</div>
        <div class="main-section__profile">mando212</div>
        <div class="main-section__profile-more"></div>
      </div>
    </header>
    <SubSection/>
  </section>
</template>

<script>
import SubSection from './SubSection.vue';

export default {
  name: 'MainSection',
  components: {
    SubSection
  }
}
</script>

<style scoped lang="scss">
@import '../index.scss';

.main-section {
  @include position(absolute, $top: 0px, $left: $main-menu-width);
  width: calc(100vw - #{$main-menu-width}); height: calc(100vh - #{$player-height});
  overflow: hidden;
}

.main-section__header {
  @include position(fixed, $top: 0px, $left: $main-menu-width);
  width: calc(100vw - #{$main-menu-width}); height: 46px;
  box-sizing: border-box;
  padding: 10px 15px;
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

.main-section__search-form {
  float: left;
  box-sizing: border-box;
  width: 180px; height: 100%;
  background: #fff;
  margin-left: 20px;
  border-radius: 20px;
  padding: 1px 5px;
  white-space: nowrap;
  overflow: hidden;
}

.main-section__search-icon {
  display: inline-block;
  width: 15px; height: 15px;
  background: no-repeat top/100% url(../assets/search-icon.png);
  vertical-align: middle;
}

.main-section__search-input {
  display: inline-block;
  width: 100%; height: 100%;
  font-family: inherit;
  font-size: 0.9rem;
  border: none;
  padding: 0;
  vertical-align: middle;

  &:focus {
    outline: none;
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