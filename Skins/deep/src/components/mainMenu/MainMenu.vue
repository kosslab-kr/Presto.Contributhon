<template>
  <nav class='menu'>
    <ul class='menu__category'>
      <li class="menu__category-title">YOUR LIBRARY</li>
      <MainMenuItem
        v-for="menuItemName in menuItemNames"
        :key="menuItemName"
        :name="menuItemName"
        ref="menuItems"
        @menu-clicked="_showContent"/>
    </ul>
    <ul class='menu__category'>
      <li class="menu__category-title">PLAYLISTS</li>
      <MainMenuItem :name="'my music1'"/>
      <MainMenuItem :name="'my music2'"/>
      <MainMenuItem :name="'my music3'"/>
      <MainMenuItem :name="'my music4'"/>
      <MainMenuItem :name="'my music5'"/>
      <MainMenuItem :name="'my music6'"/>
    </ul>
    <div class="menu__footer">
      <div class="menu__footer-icon"></div>
      <div class='menu__footer-text'>New Playlist</div>
    </div>
  </nav>
</template>

<script>
import MainMenuItem from './MainMenuItem.vue';

export default {
  name: 'MainMenu',

  components: {
    MainMenuItem
  },

  data() {
    return {
      menuItemNames: ['Musics', 'Albums', 'Genres', 'Artists']
    }
  },

  methods: {
    triggerClickEvent({menuIndex}) {
      const clickEvent = new Event('click');
      const firstMenuItem = this.$refs.menuItems[menuIndex];

      firstMenuItem.$el.dispatchEvent(clickEvent);
    },

    _showContent(menuItemName) {
      this._activateMenu(menuItemName);
      this.$emit('menu-selected', menuItemName);
    },

    _activateMenu(menuItemName) {
      this.$refs.menuItems.forEach(menuItem => {
        menuItem.isActive = (menuItem.name === menuItemName) ? true : false;
      })
    }
  }
}
</script>

<style scoped lang="scss">
@import '../../index.scss';

.menu {
  @include position(absolute, $top: 0px, $left: 0px);
  @include size($width: $main-menu-width, $height: calc(100vh - #{$player-height}));
  box-sizing: border-box;
  background: #131313;
  padding-top: 35px;
  font-size: 0.9rem;

  &:hover { cursor: default; }
}

.menu__category {
  color: #aaa;
  margin-top: 33px;
}

.menu__category-title {
  font-weight: lighter;
  letter-spacing: 1px;
  padding: 0px 25px;
  font-size: 0.8rem;
}

.menu__footer {
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
    .menu__footer-icon {
      &, &::before, &::after {
        border-color: #fff;
      }
    }
    
    .menu__footer-text {
      color: #fff;
    }
  }
}


.menu__footer-icon {
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

.menu__footer-text {
  display: inline-block;
  vertical-align: middle
}

</style>