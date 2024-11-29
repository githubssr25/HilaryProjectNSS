import { defineConfig } from "vite";
import react from "@vitejs/plugin-react";

export default defineConfig(() => {
  return {
    server: {
      open: true, // Automatically opens the app in the browser
      proxy: {
        "/api": {
          target: "http://localhost:5000", // Your backend's HTTPS URL
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
