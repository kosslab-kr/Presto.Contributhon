<template>
    <div id="app">
        <main id="main">
            <Main></Main>
        </main>
        <footer id="footer">
            <Footer></Footer>
        </footer>
    </div>
</template>

<script>
    import Side from './components/Side.vue';
    import Main from './components/Main.vue';
    import Footer from './components/Footer.vue';

    export default {
        name: 'app',
        components: {
            Side, Main, Footer,
        },
    };
</script>

<style>
    body, input, select, textarea, button {
        font-size: inherit;
        font-family: 'Gothic A1', sans-serif;
    }

    body { margin: 0; }

    .clearfix::after {
        display: block;
        content: '';
        clear: both;
    }

    ::-webkit-scrollbar {
        display: none;
    }
</style>

<style scoped>
    #side {
        position: fixed;
        left: 0;
        top: 0;
        bottom: 170px;
        width: 200px;
    }

    #main {
        padding: 0 0 170px 0;
    }

    #footer {
        position: fixed;
        bottom: 0;
        left: 0;
        right: 0;
        height: 170px;
    }
</style>
