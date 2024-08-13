import { AuthService, getAuthService } from '../auth';
import {
  EnvironmentService,
  getEnvironmentService,
} from '../environment-service';
import { joinUrl, mergeParams, RequestParameters } from './http-utils';

export class HttpBackendClient {
  constructor(
    private _authService: AuthService,
    private _environmentService: EnvironmentService
  ) {}

  private get _baseUrl() {
    return this._environmentService.getBackendUrl();
  }

  private get _axios() {
    return this._authService.getAxiosClient();
  }

  private get _axiosForm() {
    const axiosForm = structuredClone(this._authService.getAxiosClient());
    axiosForm.defaults.headers.common['Content-Type'] = 'multipart/form-data';
    return axiosForm;
  }

  get(url: string, ...params: RequestParameters[]) {
    const parameters = mergeParams(params);
    return this._axios.get(joinUrl(this._baseUrl, url), {
      ...(parameters.params && {
        params: parameters.params,
      }),
    });
  }

  post(url: string, ...params: RequestParameters[]) {
    const parameters = mergeParams(params);
    const { body } = parameters;
    return this._axios.post(joinUrl(this._baseUrl, url), body, {
      ...(parameters.params && {
        params: parameters.params,
      }),
    });
  }

  postForm(url: string, ...params: RequestParameters[]) {
    const parameters = mergeParams(params);
    const { body } = parameters;
    return this._axiosForm.post(joinUrl(this._baseUrl, url), body, {
      ...(parameters.params && {
        params: parameters.params,
      }),
    });
  }

  put(url: string, ...params: RequestParameters[]) {
    const parameters = mergeParams(params);
    const { body } = parameters;
    return this._axios.put(joinUrl(this._baseUrl, url), body, {
      ...(parameters.params && {
        params: parameters.params,
      }),
    });
  }

  putForm(url: string, ...params: RequestParameters[]) {
    const parameters = mergeParams(params);
    const { body } = parameters;
    return this._axiosForm.put(joinUrl(this._baseUrl, url), body, {
      ...(parameters.params && {
        params: parameters.params,
      }),
    });
  }

  delete(url: string, ...params: RequestParameters[]) {
    const parameters = mergeParams(params);
    return this._axios.delete(joinUrl(this._baseUrl, url), {
      ...(parameters.params && {
        params: parameters.params,
      }),
    });
  }
}

export const getHttpBackendClient = () =>
  new HttpBackendClient(getAuthService(), getEnvironmentService());
