import { defineConfig } from "vite";
import react from "@vitejs/plugin-react";

export default defineConfig(() => {
  return {
    server: {
      open: true, // Automatically opens the app in the browser
      proxy: {
        "/api": {
          target: "https://localhost:5001", // Your backend's HTTPS URL
          changeOrigin: true,
          secure: false, // Allows using self-signed certificates
        },
      },
    },
    build: {
      outDir: "build", // Output directory for production builds
    },
    plugins: [react()],
  };
});
