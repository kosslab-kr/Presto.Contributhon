<template>
    <div class="Slider__Wrapper" @mousedown="onMouseDown">
        <div class="slider" ref="slider">
            <div class="slider-line" :style="{ width: `${percent}%` }"></div>
        </div>
    </div>
</template>

<script>
    export default {
        name: 'Slider',

        model: {
            prop: 'value',
            event: 'changed',
        },

        props: {
            max: {
                type: Number,
                default: 0,
            },

            value: {
                type: Number,
                default: 0,
            },
        },

        watch: {
            currentValue (value) {
                this.$emit('changed', value);
            },

            value (value) {
                this.currentValue = value;
            }
        },

        methods: {
            updatePosition(e) {
                let x = e.target.offsetLeft + e.offsetX - this.$refs.slider.offsetLeft;
                let width = this.$refs.slider.offsetWidth;

                let percent = Math.min(Math.max(0, x / width), 1);
                this.currentValue = this.max * percent;
            },

            onDocumentMouseUp (e) {
                if (!this.isSeeking) return;
                this.updatePosition(e);

                this.startX = null;
                this.$emit('end', this.currentValue);
            },

            onDocumentMouseMove (e) {
                if (!this.isSeeking) return;
                e.preventDefault();

                this.updatePosition(e);
            },

            onMouseDown (e) {
                e.preventDefault();
                this.startX = e.pageX;

                this.updatePosition(e);
                this.$emit('start', this.currentValue);
            },
        },

        computed: {
            percent () {
                if (this.max === 0) return 0;
                return this.currentValue / this.max * 100;
            },

            isSeeking () {
                return this.startX !== null;
            },
        },

        data () {
            return {
                currentValue: this.value,
                startX: null,
            };
        },

        created () {
            document.addEventListener('mousemove', this.onDocumentMouseMove);
            document.addEventListener('mouseup', this.onDocumentMouseUp);
        },

        destroyed () {
            document.removeEventListener('mousemove', this.onDocumentMouseMove);
            document.removeEventListener('mouseup', this.onDocumentMouseUp);
        },
    };
</script>

<style scoped>
    .Slider__Wrapper {
        padding: 8px;
    }

    .slider {
        height: 4px;
        background: rgba(255, 255, 255, 0.3);
    }

    .slider-line {
        position: relative;
        height: 100%;
        background: #fff;
    }

    .slider-line::after {
        display: block;
        content: '';
        position: absolute;
        right: -10px;
        width: 20px;
        height: 20px;
        top: -8px;
        border-radius: 10px;
        background: #fff;
    }
</style>
