import { inject, injectable } from 'inversify';
import {
  HttpBackendClient,
  TYPES,
  DIContainer,
  body,
  responseJson,
  objectToFormData,
  queryParams,
} from '@socnet/shared';
import {
  GalleryDeleteQuery,
  GalleryGetQuery,
  GalleryGetReturn,
  GalleryPostBody,
  GalleryPutBody,
} from './gallery-dto';

/** /api/v1/gallery */
@injectable()
export class GalleryRepository {
  constructor(
    @inject(TYPES.HttpBackendClient) private _httpClient: HttpBackendClient
  ) {}

  /** GET /api/gallery */
  get(query: GalleryGetQuery) {
    return this._httpClient
      .get('/api/v1/gallery', queryParams(query))
      .then(responseJson<GalleryGetReturn>);
  }

  /** POST /api/gallery */
  post(payload: GalleryPostBody) {
    const formData = objectToFormData(payload);
    return this._httpClient
      .postForm('/api/v1/gallery', body(formData))
      .then(responseJson<void>);
  }

  /** PUT /api/gallery */
  put(payload: GalleryPutBody) {
    const formData = objectToFormData(payload);
    return this._httpClient
      .putForm('/api/v1/gallery', body(formData))
      .then(responseJson<void>);
  }

  /** DELETE /api/gallery/{imageId} */
  delete(imageId: string, query: GalleryDeleteQuery) {
    return this._httpClient
      .delete(`/api/v1/gallery/${imageId}`, queryParams(query))
      .then(responseJson<void>);
  }
}

export const getGalleryRepository = () =>
  DIContainer.get<GalleryRepository>(TYPES.GalleryRepository);
