<template>
  <div class="search">
    <div class="search__icon"></div>
    <input
      class='search__input'
      type="text"
      name='keyword'
      placeholder='Search'
      @input="$emit('value-changed', $event)"
    >
  </div>
</template>

<script>
export default {
  name: 'MainSearch'
}
</script>

<style scoped lang="scss">
@import "../index.scss";

.search {
  position: relative;
  float: left;
  width: 180px; height: 26px;
  background: #fff;
  border-radius: 20px;
  white-space: nowrap;
  overflow: hidden;
}

.search__icon {
  position: absolute;
  top: 6px; left: 5px;
  width: 15px; height: 15px;
  background: no-repeat center/100% url(../assets/search-icon.png);
}

.search__input {
  position: absolute;
  top: 0px; left: 25px;
  width: 140px; height: 100%;
  font-size: 0.9rem;
  padding: 0;
  border: none;
  white-space: nowrap;
  overflow: hidden;

  &:focus {
    outline: none;
  }
}
</style>
