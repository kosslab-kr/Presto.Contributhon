<template>
  <nav class='main-menu'>
    <ul class='main-menu__category'>
      <li class="main-menu__item main-menu__item--selected">Browse</li>
      <li class="main-menu__item">Radio</li>
    </ul>
    <ul class='main-menu__category'>
      <li class="main-menu__category-title">YOUR LIBRARY</li>
      <li class="main-menu__item">Recently Played</li>
      <li class="main-menu__item">Favorite Songs</li>
      <li class="main-menu__item">Albums</li>
      <li class="main-menu__item">Artists</li>
      <li class="main-menu__item">Stations</li>
      <li class="main-menu__item">Local Files</li>
      <li class="main-menu__item">Podcasts</li>
    </ul>
    <ul class='main-menu__category'>
      <li class="main-menu__category-title">PLAYLISTS</li>
      <li class="main-menu__item">my music</li>
    </ul>
    <div class="main-menu__footer">
      <div class="main-menu__footer-icon"></div>
      <div class='main-menu__footer-text'>New Playlist</div>
    </div>
  </nav>
</template>

<script>
export default {
  name: 'MainMenu'
}
</script>

<style scoped lang="scss">
@import '../index.scss';

.main-menu {
  @include position(absolute, $top: 0px, $left: 0px);
  @include size($width: $main-menu-width, $height: calc(100vh - #{$player-height}));
  box-sizing: border-box;
  background: #131313;
  padding: 5px 0px;
  font-size: 0.9rem;

  &:hover { cursor: default; }
}

.main-menu__category {
  color: #aaa;
  margin-top: 33px;
}

.main-menu__category-title {
  font-weight: lighter;
  letter-spacing: 1px;
  padding: 0px 25px;
  font-size: 0.8rem;
}

.main-menu__item {
  box-sizing: border-box;
  padding: 0px 25px;
  margin: 0.7rem 0;
  line-height: 1.3rem;

  &:hover { color: #fff; }
}

.main-menu__item--selected {
  color: #fff;
  box-shadow: inset 4px 0px 0 0 $signature-color;
}

.main-menu__footer {
  @include position(fixed, $bottom: $player-height);
  @include size($width: $main-menu-width);
  box-sizing: border-box;
  line-height: 55px;
  border-top: 1px solid #333;
  font-weight: lighter;
  font-size: 1rem;
  color: #aaa;
  background: #111;
  padding: 0px 25px;

  &:hover {
    .main-menu__footer-icon {
      &, &::before, &::after {
        border-color: #fff;
      }
    }
    
    .main-menu__footer-text {
      color: #fff;
    }
  }
}


.main-menu__footer-icon {
  @include position(relative);
  @include size($width: 21px, $height: 21px);
  display: inline-block;
  border-radius: 50%;
  $border-style: 1px solid #aaa;
  border: $border-style;
  margin-right: 7px;
  vertical-align: middle;

  @mixin cross($top, $left, $border-side) {
    @include position(absolute, $top: $top, $left: $left);
    @include size($width: 26%, $height: 26%);
    box-sizing: border-box;
    display: block;
    content: '';
    @each $side in $border-side {
      border-#{$side} : $border-style;
    }
  }

  &::before { @include cross($top: 25%, $left: 25%, $border-side: (right, bottom)) }

  &::after { @include cross($top: 48%, $left: 48%, $border-side: (top, left)) }
}

.main-menu__footer-text {
  display: inline-block;
  vertical-align: middle
}

</style>