<template>
  <section class="main-section">
    <header class='main-section__header'>
      <Search/>
      <More/>
    </header>
    <SubSection/>
  </section>
</template>

<script>
import SubSection from './SubSection.vue';
import Search from './Search.vue';
import More from './More.vue';

export default {
  name: 'MainSection',
  components: {
    SubSection,
    Search,
    More
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

</style>