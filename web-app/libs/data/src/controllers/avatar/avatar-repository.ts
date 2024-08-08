import { AvatarPostBody, AvatarPutBody } from './avatar-dto';
import {
  body,
  getHttpBackendClient,
  HttpBackendClient,
  objectToFormData,
  responseJson,
} from '@socnet/shared';

/** /api/v1/avatar */
class AvatarRepository {
  constructor(private _httpClient: HttpBackendClient) {}

  /** GET /api/avatar/{userId} */
  get(userId: number) {
    return this._httpClient
      .get(`/api/v1/avatar/${userId}`)
      .then(responseJson<string>);
  }

  /** DELETE /api/avatar/{userId} */
  delete(userId: number) {
    return this._httpClient
      .delete(`/api/v1/avatar/${userId}`)
      .then(responseJson<void>);
  }

  /** POST /api/avatar */
  post(payload: AvatarPostBody) {
    const formData = objectToFormData(payload);
    return this._httpClient
      .postForm('/api/v1/avatar', body(formData))
      .then(responseJson<string>);
  }

  /** PUT /api/avatar */
  put(payload: AvatarPutBody) {
    const formData = objectToFormData(payload);
    return this._httpClient
      .putForm('/api/v1/avatar', body(formData))
      .then(responseJson<string>);
  }
}

export const getAvatarRepository = () =>
  new AvatarRepository(getHttpBackendClient());
