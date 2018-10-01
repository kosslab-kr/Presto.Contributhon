<template>
  <div
    class="more"
    @click="activate">
    <div
      class="more__list"
      :class="{'more__list--active': isActive}">
      <div class="more__list-item">first</div>
      <div class="more__list-item">second</div>
      <div class="more__list-item">third</div>
      <div class="more__list-item">fourth</div>
      <div class="more__list-item">fifth</div>
    </div>
  </div>
</template>

<script>
export default {
  name: 'More',

  data() {
    return {
      isActive: false
    }
  },

  methods: {
    activate() {
      this.isActive = !this.isActive;
    }
  },

  // collape when clicking outside of more componenet
  mounted() {
    const collapse = ({target}) => {
      if(target.contains(this.$el)) return;

      this.isActive = false;
    }

    document.body.addEventListener('click', collapse);
  }
}
</script>

<style scoped lang="scss">
@import '../../index.scss';

.more {
  position: relative;
  float: left;
  width: 26px; height: 26px;
  margin-left: 5px;
  @include line-arrow($size: 50%, $border-width: 1px, $direction: bottom, $color: #fff);
}

.more__list {
  display: none;
  box-sizing: border-box;
  position: absolute;
  top: 35px; left: 0px;
  width: 150px;
  padding: 10px 0;
  background: #272727;
  border-radius: 8px;
  box-shadow: 0px 0px 20px 2px #111;

  &--active {
    display: block;
  }
}

.more__list-item {
  box-sizing: border-box;
  padding: 0 10px;
  line-height: 1.8rem;
  color: #fff;
  font-size: 1rem;
  font-weight: lighter;
  white-space: nowrap;
  overflow: hidden;
  text-overflow: ellipsis;

  &:hover {
    background: #555;
  }
}

</style>
