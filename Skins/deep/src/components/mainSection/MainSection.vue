<template>
  <section class="main-section">
    <header class='main-section__header'>
      <Search/>
      <More/>
    </header>
    <AlbumView/>
  </section>
</template>

<script>
import AlbumView from './AlbumView.vue';
import Search from './Search.vue';
import More from './More.vue';

export default {
  name: 'MainSection',

  components: {
    AlbumView,
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
  width: calc(100vw - #{$main-menu-width}); height: 70px;
  background: linear-gradient(#191919 50%, rgba(0,0,0,0));
  box-sizing: border-box;
  padding: 15px 30px 0 30px;
  z-index: 100;
}

</style>