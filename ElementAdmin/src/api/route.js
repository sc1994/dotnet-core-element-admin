import request from '@/utils/request'

export function getRoutes() {
  return request({
    url: '/route',
    method: 'get'
  })
}
