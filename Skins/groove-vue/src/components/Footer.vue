<template>
    <div class="Footer__Wrapper">
        <div class="slider-panel">
            <div class="current-time">{{ timeString(position) }}</div>
            <div class="slider">
                <Slider v-model="position" :max="length" @start="onSeekStart" @end="onSeekEnd"></Slider>
            </div>
            <div class="total-time">{{ timeString(length) }}</div>
        </div>

        <div class="info-panel">
            <div class="album">
                <img :src="albumImage" :alt="title">
            </div>
            <div class="title">{{ title }}</div>
            <div class="artist">{{ artist }}</div>
        </div>

        <div class="button-panel">
            <button class="prev" type="button" @click="playPrevious"><i class="fas fa-step-backward"></i> Previous</button>
            <button class="pause" type="button" @click="pause" v-if="isPlaying"><i class="fas fa-pause"></i> Pause</button>
            <button class="play" type="button" @click="play" v-else><i class="fas fa-play"></i> Play</button>
            <button class="next" type="button" @click="playNext"><i class="fas fa-step-forward"></i> Next</button>
            <div class="volume">
                <button class="volume"><i class="fas fa-volume-up"></i> Volume</button>
            </div>
            <button class="next-type random" type="button"><i class="fas fa-random"></i> Random</button>
            <button class="repeat-type all" type="button"><i class="fas fa-retweet"></i> All</button>
        </div>
    </div>
</template>

<script>
    import Slider from './Slider.vue';

    export default {
        name: 'Footer',
        components: {
            Slider,
        },

        methods: {
            timeString (time) {
                return new Date(time).toISOString().substr(14, 5);
            },

            play () {
                player.play()
            },

            pause () {
                player.pause();
            },

            playPrevious () {
                player.playPrevious();
            },

            playNext () {
                player.playNext();
            },

            onPositionChanged () {
                player.getPosition().then(position => {
                    if (this.isSeeking) return;

                    this.position = position;
                });
            },

            onSeekStart () {
                this.isSeeking = true;
                
                player.off('positionChanged', this.onPositionChanged);
            },

            onSeekEnd (value) {
                this.isSeeking = false;

                player.setPosition(value).then(() => {
                    return player.getPosition();
                }).then(position => {
                    this.position = position;
                    player.on('positionChanged', this.onPositionChanged);
                });
            },

            onCurrentMusicChanged () {
                Promise.all([
                    player.getPosition(),
                    player.getLength(),
                    player.getCurrentMusic(),
                ]).then(([ position, length, currentMusic ]) => {
                    this.position = position;
                    this.length = length;
                    this.title = currentMusic.Title;
                    this.artist = currentMusic.Artist.Name;
                    this.albumImage = currentMusic.Album.Picture;
                });
            },
        },

        data () {
            return {
                title: 'Title',
                artist: 'Artist',
                albumImage: 'https://upload.wikimedia.org/wikipedia/en/thumb/5/54/Public_image_ltd_album_cover.jpg/220px-Public_image_ltd_album_cover.jpg',

                position: 200,
                length: 1000 * 1000,

                isPlaying: false,
            };
        },

        created () {
            player.on('positionChanged', this.onPositionChanged);
            player.on('currentMusicChanged', this.onCurrentMusicChanged);
        },

        destroyed () {
            player.off('positionChanged', this.onPositionChanged);
        },
    };
</script>

<style scoped>
    .Footer__Wrapper {
        height: 100%;
        box-sizing: border-box;
        background: rgba(0, 63, 214, 0.8);
        color: #fff;
        padding: 30px 20px;
        backdrop-filter: blur(20px);
        -webkit-backdrop-filter: blur(20px);
    }

    .slider-panel {
        display: flex;
        flex-direction: row;
        line-height: 20px;
        margin-bottom: 20px;
    }

    .current-time {
        text-align: right;
        min-width: 50px;
        margin-right: 20px;
    }

    .slider {
        flex-grow: 1;
    }

    .total-time {
        text-align: left;
        min-width: 50px;
        margin-left: 20px;
    }


    .info-panel {
        float: left;
        position: relative;
        padding: 5px 0 5px 85px;
        height: 70px;
        box-sizing: border-box;
    }

    .album {
        position: absolute;
        left: 0;
        top: 0;
        width: 70px;
        height: 70px;
    }

    .Footer__Wrapper .album img {
        display: block;
        width: 70px;
        height: 70px;
    }

    .title {
        font-size: 20px;
        font-weight: bold;
        height: 35px;
        line-height: 35px;
    }

    .artist {
        font-size: 16px;
        height: 25px;
        line-height: 25px;
    }

    .button-panel {
        float: right;
        display: flex;
        flex-direction: row;
        margin-top: 30px;
    }

    button {
        position: relative;
        height: 40px;
        width: 40px;
        outline: none;
        background: none;
        border: none;
        margin: 0 5px;
        color: #fff;
        overflow: hidden;
        text-indent: -10000px;
        font-size: 20px;
        border-radius: 100%;
        cursor: pointer;
    }

    button:hover {
        background: rgba(255, 255, 255, 0.1);
    }

    .Footer__Wrapper button i {
        position: absolute;
        left: 50%;
        top: 50%;
        transform: translate(-50%, -50%);
        text-indent: 0;
    }

    .Footer__Wrapper .play i {
        left: 55%;
    }

    .play,
    .pause {
        width: 50px;
        height: 50px;
        font-size: 25px;
        margin-top: -5px;
    }
</style>
