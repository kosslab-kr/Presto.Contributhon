<template>
    <div class="GalleryView__Wrapper">
        <GalleryItem v-for="item in items" v-bind="item" @click.native="onItemClick(item)"></GalleryItem>
    </div>
</template>

<script>
    import GalleryItem from './GalleryItem.vue';

    export default {
        name: 'GalleryView',
        props: {
            items: {
                type: Array,
                default: () => [],
            },
        },
        components: {
            GalleryItem,
        },

        methods: {
            onItemClick (item) {
                this.$emit('item-click', item);
            },
        },
        data() {
            return {};
        },
    };
</script>

<style scoped>
    .GalleryView__Wrapper {
        display: grid;
        grid-template-columns: repeat(auto-fit, minmax(220px, 1fr));
        padding: 10px;
    }
</style>
