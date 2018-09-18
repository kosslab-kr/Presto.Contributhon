<template>
  <section class="sub-section">
      <div class="sub-section__header">
        <div class="sub-section__title">Browse</div>
        <nav class="sub-section__menu">
          <div class="sub-section__menu-item sub-section__menu-item--selected">
            <div class="sub-section__menu-name">OVERVIEW</div>
            <div class="sub-section__menu-item-bar"></div>
          </div>
          <div class="sub-section__menu-item">
            <div class="sub-section__menu-name">PODCASTS</div>
            <div class="sub-section__menu-item-bar"></div>
          </div>
          <div class="sub-section__menu-item">
            <div class="sub-section__menu-name">CHARTS</div>
            <div class="sub-section__menu-item-bar"></div>
          </div>
          <div class="sub-section__menu-item">
            <div class="sub-section__menu-name">GENRES & MOODS</div>
            <div class="sub-section__menu-item-bar"></div>
          </div>
          <div class="sub-section__menu-item">
            <div class="sub-section__menu-name">NEW RELEASES</div>
            <div class="sub-section__menu-item-bar"></div>
          </div>
          <div class="sub-section__menu-item">
            <div class="sub-section__menu-name">DISCOVER</div>
            <div class="sub-section__menu-item-bar"></div>
          </div>
          <div class="sub-section__menu-item">
            <div class="sub-section__menu-name">CONCERTS</div>
            <div class="sub-section__menu-item-bar"></div>
          </div>
        </nav>
      </div>
      <section class='sub-section__body'>
        <div class="album">
          <div class="album__cover"></div>
        </div>
        <div class="album">
          <div class="album__cover"></div>
        </div>
        <div class="album">
          <div class="album__cover"></div>
        </div>
        <div class="album">
          <div class="album__cover"></div>
        </div>
        <div class="album">
          <div class="album__cover"></div>
        </div>
        <div class="album">
          <div class="album__cover"></div>
        </div>
        <div class="album">
          <div class="album__cover"></div>
        </div>
        <div class="album">
          <div class="album__cover"></div>
        </div>
        <div class="album">
          <div class="album__cover"></div>
        </div>
        <div class="album">
          <div class="album__cover"></div>
        </div>
        <div class="album">
          <div class="album__cover"></div>
        </div>
        <div class="album">
          <div class="album__cover"></div>
        </div>
        <div class="album">
          <div class="album__cover"></div>
        </div>
        <div class="album">
          <div class="album__cover"></div>
        </div>
        <div class="album">
          <div class="album__cover"></div>
        </div>
        <div class="album">
          <div class="album__cover"></div>
        </div>
      </section>
    </section>
</template>

<script>
export default {
  name: 'SubSection'
}
</script>

<style scoped lang="scss">
@import '../../index.scss';

.sub-section {
  width: 100%;
  height: 100%;
  overflow: scroll;
  background: #191919;

  &:before {
    content: '';
    display: block;
    height: 90px;
    background: linear-gradient(#444, #2f2f2f);
  }
}

.sub-section__header {
  box-sizing: border-box;
  position: sticky;
  top: 0px;
  height: 160px;
  background: linear-gradient(#2f2f2f, #191919 100px);
  border-bottom: 1px solid #333;
  padding: 55px 30px 0px 30px;
}

.sub-section__title {
  height: 40px;
  color: #fff;
  padding: 3px 0px;
  // font-size: 1.9rem;
  font-size: 2.8rem;
  font-weight: 900;
  
  &::before {
    content: '';
    display: inline-block;
    height: 100%;
    vertical-align: bottom;
  }

  &:hover {
    cursor: default;
  }
}

.sub-section__menu {
  margin-top: 28px;
}

.sub-section__menu-item {
  display: inline-block;
  margin-right: 25px;

  &:nth-last-child(1) {
    margin-right: 0px;
  }

  &--selected {
    .sub-section__menu-name {
      color: #fff;
    }

    .sub-section__menu-item-bar {
      background: $signature-color;
    }
  }

  &:hover {
    cursor: pointer;

    .sub-section__menu-name {
      color: #fff;
    }
  }
}

.sub-section__menu-name {
  color: #aaa;
  font-size: 0.8rem;
  font-weight: 600;
  letter-spacing: 2px;
}

.sub-section__menu-item-bar {
  margin: auto;
  margin-top: 10px;
  width: 30px;
  height: 2px;
}

.sub-section__body {
  padding: 30px;
}

.album {
  width: 32%;
  background: #fff;
  float: left;
  margin-right: 2%;
  margin-bottom: 2%;

  &:nth-child(3n) {
    margin-right: 0;
  }
}

@media only screen and (min-width: 1100px) {
  .album {
    width: 23.5%;

    &:nth-child(3n) {
      margin-right: 2%;
    }

    &:nth-child(4n) {
      margin-right: 0px;
    }
  }
}

.album__cover {
  height: 0;
  padding-top: 100%;
}

</style>