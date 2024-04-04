import { MfFetch } from "@mf/react-app";
import IParams from "../interfaces/IParams";

const demoService = {

	getData(params: IParams) {
		return new MfFetch("Misc/getData", params, "POST");
	},

	testPing() {
		return new MfFetch("Misc", undefined, "GET");
	},

	GetOptionsPortType() {
		return new MfFetch("Misc/getOptionsPortType", undefined, "GET");
	}
};

export default demoService;
