import {
  GalleryDeleteQuery,
  GalleryGetQuery,
  GalleryGetReturn,
  GalleryPostBody,
  GalleryPutBody,
} from './gallery-dto';
import {
  body,
  getHttpBackendClient,
  HttpBackendClient,
  objectToFormData,
  queryParams,
  responseJson,
} from '@socnet/shared';

/** /api/v1/gallery */
class GalleryRepository {
  constructor(private _httpClient: HttpBackendClient) {}

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
  new GalleryRepository(getHttpBackendClient());
