<template>
    <div class="GalleryItem__Wrapper">
        <div class="image">
            <img :src="album" :alt="title">
        </div>
        <div class="title">{{ title }}</div>
        <div class="artist">{{ artist }}</div>
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
        display: block;
        width: 200px;
        box-sizing: border-box;
        margin: 10px auto 30px auto;
        cursor: pointer;
    }

    .GalleryItem__Wrapper:hover {
        background: #f7f7f7;
    }
</style>
