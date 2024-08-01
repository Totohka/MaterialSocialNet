import { inject, injectable } from 'inversify';
import {
  HttpBackendClient,
  TYPES,
  DIContainer,
  body,
  responseJson,
  objectToFormData,
} from '@socnet/shared';
import { AvatarPostBody, AvatarPutBody } from './avatar-dto';

/** /api/v1/avatar */
@injectable()
export class AvatarRepository {
  constructor(
    @inject(TYPES.HttpBackendClient) private _httpClient: HttpBackendClient
  ) {}

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
  DIContainer.get<AvatarRepository>(TYPES.AvatarRepository);
