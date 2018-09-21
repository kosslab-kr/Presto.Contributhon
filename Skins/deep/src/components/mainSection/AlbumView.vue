<template>
  <section class="sub-section">
    <div class="sub-section__header">
      <div class="sub-section__title">{{title}}</div>
      <div class="sub-section__menu">
        <ViewHeaderMenuItem
          v-for="(menuName, idx) in menuList"
          :key="idx"
          :name="menuName"
          ref="menuItems"
          v-on:menu-selected="showContent"/>
      </div>
    </div>
    <section class='sub-section__body'>
      <Album
        v-for="(n, idx) in 10"
        :key="idx"/>
    </section>
  </section>
</template>

<script>
import ViewHeaderMenuItem from './ViewHeaderMenuItem.vue';
import Album from './Album.vue';

export default {
  name: 'AlbumView',
  
  components: {
    ViewHeaderMenuItem,
    Album
  },

  data() {
    return {
      title: 'Album',
      menuList: ['FIRST', 'SECOND', 'THIRD', 'FOURTH', 'FIFTH']
    }
  },

  // initialize
  mounted() {
    const firstMenuName = this.menuList[0];
    const initialClickEvent = new Event('click');

    this.$refs.menuItems.some( menu => {
      if(menu.name !== firstMenuName) return false;

      menu.$el.dispatchEvent(initialClickEvent);

      return true;
    })
  },

  methods: {
    showContent(name) {
      this.activateMenu(name);
      // more
    },

    activateMenu(name) {
      this.$refs.menuItems.forEach(menu => {
        menu.isActive = (menu.name === name) ? true : false;
      });
    }
  }
}
</script>

<style scope lang="scss">
@import '../../index.scss';

.sub-section__header {
  position: sticky;
  top: 0px;
  border-bottom: 1px solid #333;
  z-index: 1;
}

.sub-section__title {
  box-sizing: border-box;
  color: #fff;
  padding: 0 30px;
  font-size: 2.8rem;
  font-weight: 900;
  background: #191919;

  &:hover {
    cursor: default;
  }
}

.sub-section__menu {
  box-sizing: border-box;
  background: #191919;
  padding: 20px 30px 2px 30px;
}

.sub-section {
  width: 100%;
  height: 100%;
  overflow: scroll;
  background: #191919;

  &:before {
    content: '';
    display: block;
    height: 100px;
    // background: linear-gradient(#444, #2f2f2f);
  }
}

.sub-section__body {
  padding: 30px;
}

</style>
