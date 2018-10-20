<template>
    <div class="Main__Wrapper">
        <GalleryView :items="musics" @item-click="onItemClick"></GalleryView>
    </div>
</template>

<script>
    import GalleryView from './GalleryView.vue';

    export default {
        name: 'Main',
        components: {
            GalleryView,
        },

        methods: {
            onItemClick (item) {
                console.log('Pointer:', item.music.Pointer);
                player.playByMusic(item.music);
            },

            updateMusics () {
                library.getMusics().then(musics => {
                    this.musics = musics.map(music => {
                        return {
                            music,
                            title: music.Title,
                            album: music.Album.Picture,
                            artist: music.Artist.Name,
                        };
                    });
                });
            },

            onCurrentMusicChanged () {
                this.updateMusics();
            }
        },

        data () {
            return {
                musics: [],
            };
        },

        created () {
            this.updateMusics();
            player.on('currentMusicChanged', this.onCurrentMusicChanged);
        },

        destroyed () {
            player.off('currentMusicChanged', this.onCurrentMusicChanged);
        },
    };
</script>

<style scoped>
</style>
