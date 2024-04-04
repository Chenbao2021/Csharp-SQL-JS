import { createTheme } from "@mui/material";
import { mfSurchargeTheme, medissysPartialLightTheme } from "@mf/react-mui-controls"

export const myCustomTheme = createTheme(mfSurchargeTheme(medissysPartialLightTheme, {
	components: {
		// Surcharge du theme medissysLightTheme pour les composants
		MuiButton: {
			styleOverrides: {
				containedPrimary: {
					"&.myCustomButton": {
						backgroundColor: "blue",
						":hover": {
							backgroundColor: "yellow",
							color: "black"
						}
					}
				}
			}
		},

	}
}));