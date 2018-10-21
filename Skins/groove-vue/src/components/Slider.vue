<template>
    <div class="Slider__Wrapper" @mousedown="onMouseDown" @touchstart="onTouchStart">
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
            touchFromMouse (e) {
                if (e.targetTouches.length) {
                    return e.targetTouches[0];
                }

                if (e.touches.length) {
                    return e.touches[0];
                }

                return e.changedTouches[0];
            },

            updatePosition(e) {
                let x = e.pageX - this.$refs.slider.offsetLeft;
                let width = this.$refs.slider.offsetWidth;

                let percent = Math.min(Math.max(0, x / width), 1);
                this.currentValue = this.max * percent;
            },

            onMouseDown (e) {
                e.preventDefault();

                this.updatePosition(e);
                this.$emit('start', this.currentValue);
                this.isSeeking = true;
            },

            onTouchStart (e) {
                e.preventDefault();
                let mouse = this.touchFromMouse(e);
                console.log(mouse);

                this.updatePosition(mouse);
                this.$emit('start', this.currentValue);
                this.isSeeking = true;
            },

            onDocumentMouseMove (e) {
                if (!this.isSeeking) return;
                e.preventDefault();

                this.updatePosition(e);
            },

            onDocumentTouchMove (e) {
                if (!this.isSeeking) return;

                let mouse = this.touchFromMouse(e);
                this.updatePosition(mouse);
            },

            onDocumentMouseUp (e) {
                if (!this.isSeeking) return;
                e.preventDefault();

                this.updatePosition(e);

                this.$emit('end', this.currentValue);
                this.isSeeking = false;
            },

            onDocumentTouchEnd (e) {
                if (!this.isSeeking) return;
                e.preventDefault();

                let mouse = this.touchFromMouse(e);
                this.updatePosition(mouse);

                this.$emit('end', this.currentValue);
                this.isSeeking = false;
            },
        },

        computed: {
            percent () {
                if (this.max === 0) return 0;
                return this.currentValue / this.max * 100;
            },
        },

        data () {
            return {
                currentValue: this.value,
                isSeeking: false,
            };
        },

        created () {
            document.addEventListener('mousemove', this.onDocumentMouseMove);
            document.addEventListener('mouseup', this.onDocumentMouseUp);

            document.addEventListener('touchmove', this.onDocumentTouchMove);
            document.addEventListener('touchend', this.onDocumentTouchEnd);
        },

        destroyed () {
            document.removeEventListener('mousemove', this.onDocumentMouseMove);
            document.removeEventListener('mouseup', this.onDocumentMouseUp);

            document.removeEventListener('touchmove', this.onDocumentTouchMove);
            document.removeEventListener('touchend', this.onDocumentTouchEnd);
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
