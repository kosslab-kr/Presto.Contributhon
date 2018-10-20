<template>
    <div class="GalleryItem__Wrapper">
        <div class="item">
            <div class="image">
                <img :src="album" :alt="title">
            </div>
            <div class="title">{{ title }}</div>
            <div class="artist">{{ artist }}</div>
        </div>
    </div>
</template>

<script>
    export default {
        name: 'GalleryItem',
        props: {
            title: {
                type: String,
                default: '',
            },

            album: {
                type: String,
                default: '',
            },

            artist: {
                type: String,
                default: '',
            },
        },
        data() {
            return {};
        },
    };
</script>

<style scoped>
    .GalleryItem__Wrapper {
        padding: 15px 0;
        cursor: pointer;
    }

    .item {
        width: 200px;
        display: block;
        margin: 0 auto;
    }

    .GalleryItem__Wrapper:hover {
        background: #eee;
    }

    .image {
        width: 200px;
        height: 200px;
    }

    .GalleryItem__Wrapper .image img {
        width: 200px;
        height: 200px;
    }

    .title {
        font-size: 16px;
        padding-top: 8px;
    }

    .artist {
        font-size: 12px;
        color: #888;
        padding: 2px 0;
    }
</style>
