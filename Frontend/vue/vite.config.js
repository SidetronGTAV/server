mport { defineConfig } from 'vite';
import vue from '@vitejs/plugin-vue';
export default defineConfig({
    plugins: [vue()],
    base: './',
    build: {
        outDir: '../../server/resources/server/client/html',
        emptyOutDir: true
    }
});
