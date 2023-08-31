export default {
    content: ["./index.html", "./src/**/*.{vue,js,ts,jsx,tsx}",],
    theme: {
        extend: {},
    },
    plugins: [require("daisyui")],
    daisyui: {
        base: false,
        themes: [{
            mytheme: {
                "primary": "#86198f",
                "secondary": "#D926AA",
                "accent": "#1FB2A5",
                "neutral": "#191D24",
                "base-100": "#2A303C",
                "info": "#3ABFF8",
                "success": "#36D399",
                "warning": "#FBBD23",
                "error": "#dc2626",
            },
        },],
    },
}