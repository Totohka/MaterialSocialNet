import { inject, injectable } from 'inversify';
import { TYPES } from '../di-container';
import { AuthService } from '../auth';
import { EnvironmentService } from '../environment-service';
import { joinUrl, mergeParams, RequestParameters } from './http-utils';

@injectable()
export class HttpBackendClient {
  constructor(
    @inject(TYPES.AuthClient) private _authService: AuthService,
    @inject(TYPES.EnvReader) private _envReader: EnvironmentService
  ) {}

  private get _baseUrl() {
    return this._envReader.getBackendUrl();
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
