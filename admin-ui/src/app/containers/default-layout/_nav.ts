import { INavData } from '@coreui/angular';

export const navItems: INavData[] = [
  {
    name: 'Trang Chủ',
    url: '/dashboard',
    iconComponent: { name: 'cil-speedometer' },
    badge: {
      color: 'info',
      text: 'NEW'
    }
  },

  {
    name: 'Nội Dung',
    url: '/content',
    iconComponent: { name: 'cil-puzzle' },
    children: [
      {
        name: 'Danh Mục',
        url: '/content/post-categories'
      },
      {
        name: 'Bài Viết',
        url: '/content/posts'
      },
      {
        name: 'Loạt bài',
        url: '/content/series'
      },
      {
        name: 'Nhuận bút',
        url: '/content/royalty'
      }
      
    ]
  },
  {
    name: 'Hệ Thống',
    url: '/system',
    iconComponent: { name: 'cil-notes' },
    children: [
      {
        name: 'Quyền',
        url: '/system/roles'
      },
      {
        name: 'Người Dùng',
        url: '/system/users'
      }
      
    ]
  },
];
