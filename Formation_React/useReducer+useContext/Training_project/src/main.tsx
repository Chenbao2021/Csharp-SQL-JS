import React from "react";
import ReactDOM from "react-dom";
import pkg from "../package.json";

import App from "./App";
import { myCustomTheme } from "./MUI/myCustomTheme";
import { MfApp, _mfConfigLoader } from "@mf/react-app";
import { MfLicenseManager } from "@mf/react-grid";
import { GridProvider } from "./reducer/GridProvider";

import '@mf/react-mui-controls/lib/index.css';
import '@mf/react-grid/lib/index.css';
import './styles/app.less'
import './styles/details.less'
import "./styles/header.less"
import './styles/list_Referential.less'


MfLicenseManager.setMedissysLicense();
_mfConfigLoader.loadConfig(pkg.name, () => {
	MfLicenseManager.setMedissysLicense();
	ReactDOM.render(
		<React.StrictMode>
			<MfApp theme={myCustomTheme}>
				<GridProvider>
					<App />
				</GridProvider>
			</MfApp>
		</React.StrictMode>,
		document.getElementById("root")
	);
});
