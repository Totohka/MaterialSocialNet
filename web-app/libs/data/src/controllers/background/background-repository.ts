import { BackgroundPostBody, BackgroundPutBody } from './background-dto';
import {
  body,
  getHttpBackendClient,
  HttpBackendClient,
  objectToFormData,
  responseJson,
} from '@socnet/shared';

/** /api/v1/background */
class BackgroundRepository {
  constructor(private _httpClient: HttpBackendClient) {}

  /** GET /api/background/{userId} */
  get(userId: number) {
    return this._httpClient
      .get(`/api/v1/background/${userId}`)
      .then(responseJson<string>);
  }

  /** DELETE /api/background/{userId} */
  delete(userId: number) {
    return this._httpClient
      .delete(`/api/v1/background/${userId}`)
      .then(responseJson<void>);
  }

  /** POST /api/background */
  post(payload: BackgroundPostBody) {
    const formData = objectToFormData(payload);
    return this._httpClient
      .postForm('/api/v1/background', body(formData))
      .then(responseJson<string>);
  }

  /** PUT /api/background */
  put(payload: BackgroundPutBody) {
    const formData = objectToFormData(payload);
    return this._httpClient
      .putForm('/api/v1/background', body(formData))
      .then(responseJson<string>);
  }
}

export const getBackgroundRepository = () =>
  new BackgroundRepository(getHttpBackendClient());
